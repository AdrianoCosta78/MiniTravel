export interface CriarReservaRequest {
    clienteId: string;
    destinoId: string;
    dataViagem: string;
    quantidadeViajantes: number;
}

const API_URL = 'https://localhost:7041/api/Reservas';

export async function criarReserva(
    reserva: CriarReservaRequest
) {
    const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(reserva)
    });

    if (!response.ok) {
        throw new Error('Erro ao criar reserva.');
    }

    return await response.json();
}