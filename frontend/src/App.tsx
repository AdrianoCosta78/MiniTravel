import { useEffect, useState } from 'react';
import { listarDestinos, type Destino } from './services/destinoService';
import DestinoCard from './components/DestinoCard';
import ReservaForm from './components/ReservaForm';
import './App.css';

function App() {
    const [destinos, setDestinos] = useState<Destino[]>([]);
    const [erro, setErro] = useState('');

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
        <div className="app">

            <header className="app-header">
                <h1>MiniTravel ✈️</h1>
                <p>Sistema de reservas de viagens</p>
            </header>

            <h2>Destinos disponíveis</h2>

            {erro && <p>{erro}</p>}

            <div className="destinos-grid">
                {destinos.map((destino) => (
                    <DestinoCard
                        key={destino.id}
                        cidade={destino.cidade}
                        pais={destino.pais}
                        precoBase={destino.precoBase}
                    />
                ))}
            </div>

            <ReservaForm destinos={destinos} />

        </div>
    );
}

export default App;