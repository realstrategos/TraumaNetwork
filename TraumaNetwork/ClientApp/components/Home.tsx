import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { Link, NavLink } from 'react-router-dom';
import { Dropdown } from './Dropdown';

interface FetchDataExampleState {
    loading: boolean;
    services: Service[];
    ageGroups: Orderable[];
    ageGroupsOpen: boolean,
    categoriesOpen: boolean,
    categories: Orderable[];
    step: number;
    filterInfo: FilterInfo;
}

interface Service {
    id: string;
    name: string;
}

interface FilterInfo {
    service: Service;
    ageGroup: Orderable;
    categories: Orderable;
}

export class Home extends React.Component<RouteComponentProps<{}>, any> {
    constructor() {
        super();
        var info = {};
        // this.toggle = this.toggle.bind(this);
        this.state = {
            loading: true,
            ageGroupsOpen: false,
            categoriesOpen: false,
            categories: [],
            ageGroups: [],
            services: [],
            specialties: [],
            financials: [],
            results: [],
            step: 0,
        };
        fetch('api/categories')
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ categories: data, loading: false }); // services: data,
            });
    }

    categorySelected(category: Orderable) {
        fetch(`api/agegroups?categoryID=${category.id}`)
            .then(response => response.json() as Promise<Service[]>)
            .then(data => {
                this.setState({ ageGroups: data, loading: false }); // services: data,
            });
        fetch(`api/services?categoryID=${category.id}`)
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ services: data, loading: false }); // services: data,
            });
        fetch(`api/specialties?categoryID=${category.id}`)
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ specialties: data, loading: false }); // services: data,
            });
        fetch(`api/financialplans?categoryID=${category.id}`)
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ financials: data, loading: false }); // services: data,
            });
    }

    public render() {
        return <div>
            <h1>What Service Are You Seeking?</h1>
            {this.state.loading
                ? <p><em>Loading...</em></p>
                : this.renderWorkflowStep(this.state.step)}
            {this.state.loading
                ? null
                : this.renderWorkflowArrows(this.state.step)}
        </div>;
    }

    toggleAgeGroups() {
        this.setState({
            ageGroupsOpen: !this.state.ageGroupsOpen,
        });
    }

    toggleServiceCategories() {
        this.setState({
            categoriesOpen: !this.state.categoriesOpen,
        });
    }

    renderWorkflowStep(step: number) {
        switch (step) {
            case 0:
                return this.renderServiceList(this.state.categories);
            case 1:
                return this.renderAgeGroups(this.state.ageGroups);
            case 2:
            return this.renderResults();
        }
    }

    renderWorkflowArrows(step: number) {
        if (step > 0) {
            return <div>
                <button onClick={() => this.setState({ step: step - 1 })}>Go Back</button>
                {step === 1 ? <button onClick={() => this.executeSearch()}>Search</button> : null}
            </div>
        }
    }

    executeSearch() {
        const { filterInfo } = this.state;
        this.setState({ step: 2, loadingTable: true });
        fetch(`api/agencies?categoryID=${filterInfo.category.id}${this.getServiceID()}${this.getFinancialID()}${this.getSpecialtyID()}`)
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ results: data, loadingTable: false }); // services: data,
            });
    }

    getServiceID() {
        const { filterInfo } = this.state;        
        return `${filterInfo.service && filterInfo.service.id ? `&serviceID=${filterInfo.service.id}` : ''}`
    }
    getFinancialID() {
        const { filterInfo } = this.state;        
        return `${filterInfo.financial && filterInfo.financial.id ? `&financialplanID=${filterInfo.financial.id}` : ''}`
    }
    getSpecialtyID() {
        const { filterInfo } = this.state;        
        return `${filterInfo.specialty && filterInfo.specialty.id ? `&specialtyID=${filterInfo.specialty.id}` : ''}`
    }


    renderServiceList(categories: Orderable[]) {
        return <div>
            {categories.map(x => <button key={x.id} onClick={() => {
                this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            category: x,
                        }
                });
                this.setState({ step: 1 });
                this.categorySelected(x);
            }}>{x.name}</button>)}
        </div>;
    }

    renderAgeGroups(ageGroups: Orderable[]) {
        return <div>
            <Dropdown
                selectedItem={this.state.filterInfo.ageGroup || {}}
                selectItem={(item: Orderable) => this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            ageGroup: item,
                        }
                })}
                items={this.state.ageGroups}
                title="Age Group"
            />
            <Dropdown
                selectedItem={this.state.filterInfo.service || {}}
                selectItem={(item: Service) => this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            service: item,
                        }
                })}
                items={this.state.services}
                title="Services"
            />
            <Dropdown
                selectedItem={this.state.filterInfo.specialty || {}}
                selectItem={(item: Orderable) => this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            specialty: item,
                        }
                })}
                items={this.state.specialties}
                title="Specialties"
            />
            <Dropdown
                selectedItem={this.state.filterInfo.financial || {}}
                selectItem={(item: Orderable) => this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            financial: item,
                        }
                })}
                items={this.state.financials}
                title="Finance Options"
            />
        </div>;
    }

    renderResults() {
        const { results, loadingTable } = this.state;        
        if (loadingTable) {
            return <div><p><em>Loading Results...</em></p></div>
        }
        return <div>
            <table className='table'>
            <thead>
                <tr>
                    <th>Agency Name</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th>Website</th>
                </tr>
            </thead>
            <tbody>
            {results.map(agency =>
                <tr key={ agency.name }>
                    <td>{ agency.phone }</td>
                    <td>{ agency.email }</td>
                    <td>{ agency.website }</td>
                </tr>
            )}
            </tbody>
        </table>
        </div>;
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

export interface Orderable {
    id: string,
    name: string,
    order: number,
}
