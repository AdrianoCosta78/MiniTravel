using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MiniTravel.NotificationWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    private IConnection? _connection;
    private IChannel? _channel;

    public Worker(
        ILogger<Worker> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
            UserName = _configuration["RabbitMQ:User"] ?? "guest",
            Password = _configuration["RabbitMQ:Password"] ?? "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        const string queueName = "reserva-confirmada";

        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            var body = args.Body.ToArray();

            var mensagem = Encoding.UTF8.GetString(body);

            _logger.LogInformation(
                "Mensagem recebida: {Mensagem}",
                mensagem);

            _logger.LogInformation(
                "Enviando confirmação da viagem ao cliente...");

            await Task.Delay(1000, stoppingToken);

            await _channel.BasicAckAsync(
                deliveryTag: args.DeliveryTag,
                multiple: false);

            _logger.LogInformation(
                "Mensagem processada com sucesso!");
        };

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation(
            "Worker aguardando mensagens da fila {Fila}...",
            queueName);

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }
}
