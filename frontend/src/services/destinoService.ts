export interface Destino {
    id: string;
    cidade: string;
    pais: string;
    precoBase: number;
}

const API_URL = 'https://localhost:7041/api/Destinos';

export async function listarDestinos(): Promise<Destino[]> {
    const response = await fetch(API_URL);

    if (!response.ok) {
        throw new Error('Erro ao buscar destinos.');
    }

    return await response.json();
}