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
            categories: [
                { id: '5', name: 'Counseling / Psychotherapy for Health' },
                { id: '6', name: 'Counseling / Psychotherapy for Substance Usage' },
                { id: '7', name: 'Support Addressing Physical, Sexual, or Emotional Abuse' },
            ],
            ageGroups: [
                { id: '1', name: 'Preschool', order: 1 },
                { id: '2', name: 'School Aged', order: 2 },
                { id: '3', name: 'Teen', order: 3 },
                { id: '4', name: 'Adult', order: 4 },
            ],
            services: [
                { id: '1', name: 'Preschool', order: 1 },
                { id: '2', name: 'School Aged', order: 2 },
                { id: '3', name: 'Teen', order: 3 },
                { id: '4', name: 'Adult', order: 4 },
            ],
            step: 0,
        };

        fetch('api/services')
            .then(response => response.json() as Promise<Service[]>)
            .then(data => {
                this.setState({ services: data, loading: false }); // services: data,
            });
        fetch('api/categories')
            .then(response => response.json() as Promise<Orderable[]>)
            .then(data => {
                this.setState({ categories: data, loading: false }); // services: data,
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
            // return this.renderServiceCategories(this.state.ageGroups);
        }
    }

    renderWorkflowArrows(step: number) {
        if (step > 0) {
            return <div>
                <button onClick={() => this.setState({ step: step - 1 })}>Go Back</button>
            </div>
        }
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
            />
            {/* <Dropdown
                selectedItem={filterInfo.ageGroup || {}}
                selectItem={(item: Orderable) => filterInfo.ageGroup = item}
                items={this.state.ageGroups}
            />
            <Dropdown
                selectedItem={filterInfo.ageGroup || {}}
                selectItem={(item: Orderable) => filterInfo.ageGroup = item}
                items={this.state.ageGroups}
            />
            <Dropdown
                selectedItem={filterInfo.ageGroup || {}}
                selectItem={(item: Orderable) => filterInfo.ageGroup = item}
                items={this.state.ageGroups}
            /> */}
            {ageGroups.map(x => <button key={x.id} onClick={() => {
                this.setState({
                    filterInfo:
                        {
                            ...this.state.filterInfo,
                            ageGroup: x,
                        }
                });
                debugger;
                this.setState({ step: 2 });
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

export interface Orderable {
    id: string,
    name: string,
    order: number,
}
