import { useState } from 'react';
import { criarReserva } from '../services/reservaService';
import type { Destino } from '../services/destinoService';

interface ReservaFormProps {
    destinos: Destino[];
}

function ReservaForm({ destinos }: ReservaFormProps) {
    const [clienteId, setClienteId] = useState('');
    const [destinoId, setDestinoId] = useState('');
    const [dataViagem, setDataViagem] = useState('');
    const [quantidadeViajantes, setQuantidadeViajantes] = useState(1);
    const [mensagem, setMensagem] = useState('');

    async function handleCriarReserva() {
        try {
            setMensagem('');

            const reserva = await criarReserva({
                clienteId,
                destinoId,
                dataViagem,
                quantidadeViajantes
            });

            setMensagem(
                `Reserva criada com sucesso! Valor total: R$ ${reserva.valorTotal}`
            );
        } catch {
            setMensagem('Não foi possível criar a reserva.');
        }
    }

    return (
        <section className="reserva-form">
            <h2>Nova reserva</h2>

            <div className="form-group">
                <label>Cliente Id</label>
                <input
                    type="text"
                    value={clienteId}
                    onChange={(e) => setClienteId(e.target.value)}
                />
            </div>

            <div className="form-group">
                <label>Destino</label>
                <select
                    value={destinoId}
                    onChange={(e) => setDestinoId(e.target.value)}
                >
                    <option value="">Selecione um destino</option>

                    {destinos.map((destino) => (
                        <option key={destino.id} value={destino.id}>
                            {destino.cidade} - {destino.pais}
                        </option>
                    ))}
                </select>
            </div>

            <div className="form-group">
                <label>Data da viagem</label>
                <input
                    type="date"
                    value={dataViagem}
                    onChange={(e) => setDataViagem(e.target.value)}
                />
            </div>

            <div className="form-group">
                <label>Quantidade de viajantes</label>
                <input
                    type="number"
                    min="1"
                    value={quantidadeViajantes}
                    onChange={(e) =>
                        setQuantidadeViajantes(Number(e.target.value))
                    }
                />
            </div>

            <button onClick={handleCriarReserva}>
                Criar reserva
            </button>

            {mensagem && <p className="mensagem">{mensagem}</p>}
        </section>
    );
}

export default ReservaForm;