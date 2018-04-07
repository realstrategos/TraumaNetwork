import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { Link, NavLink } from 'react-router-dom';

let filterInfo: FilterInfo;

interface FetchDataExampleState {
    loading: boolean;
    services: Service[];
    step: number;
}

interface Service {
    id: string;
    name: string;
}

interface FilterInfo {
    service: Service;
}

export class Home extends React.Component<RouteComponentProps<{}>, FetchDataExampleState> {
    constructor() {
        super();
        this.state = { 
            loading: true, 
            services: [
            { id: '1', name: 'Counseling / Psychotherapy for Health'},
            { id: '2', name: 'Counseling / Psychotherapy for Substance Usage'},
            { id: '3', name: 'Support Addressing Physical, Sexual, or Emotional Abuse'},
            ], 
            step: 0, 
        };

        fetch('api/SampleData/WeatherForecasts')
            .then(response => response.json() as Promise<Service[]>)
            .then(data => {
                this.setState({ services: data, loading: false });
            });
    }

    public render() {
        return <div>
            <h1>What Service Are You Seeking?</h1>
            {this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForecastsTable(this.state.services)}
        </div>;
    }
    

    renderForecastsTable(services: Service[]) {
        return <div>
            {services.map(x => <button onClick={() => {
                filterInfo.service = x;
                this.setState({step: 1});
            }}>{x.name}</button>)}
        </div>;
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}