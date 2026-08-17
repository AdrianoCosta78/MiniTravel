interface DestinoCardProps {
    cidade: string;
    pais: string;
    precoBase: number;
}

function DestinoCard({
    cidade,
    pais,
    precoBase
}: DestinoCardProps) {
    return (
        <div className="destino-card">
            <h3>{cidade}</h3>

            <p>{pais}</p>

            <strong>
                R$ {precoBase.toLocaleString('pt-BR', {
                    minimumFractionDigits: 2
                })}
            </strong>
        </div>
    );
}

export default DestinoCard;