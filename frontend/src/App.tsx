import { useEffect, useState } from 'react';
import { listarDestinos, type Destino } from './services/destinoService';

function App() {
    const [destinos, setDestinos] = useState<Destino[]>([]);
    const [erro, setErro] = useState<string>('');

    useEffect(() => {
        async function carregarDestinos() {
            try {
                const dados = await listarDestinos();
                setDestinos(dados);
            } catch {
                setErro('Não foi possível carregar os destinos.');
            }
        }

        carregarDestinos();
    }, []);

    return (
        <div>
            <h1>MiniTravel</h1>
            <p>Sistema de reservas de viagens</p>

            <h2>Destinos disponíveis</h2>

            {erro && <p>{erro}</p>}

            <ul>
                {destinos.map((destino) => (
                    <li key={destino.id}>
                        {destino.cidade} - {destino.pais} - R$ {destino.precoBase}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
