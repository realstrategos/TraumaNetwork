import * as React from 'react';
import { Orderable } from './Home';

interface CounterState {
    listVisible: boolean;
}

export interface Props {
    selectItem: Function;
    selectedItem: any;
    items: any[];
    title: string,
}

export class Dropdown extends React.Component<Props, CounterState> {
    constructor() {
        super();
        this.state = { listVisible: false };
    }

    public render() {

        var options = this.props.items.map(function(option) {
            return (
                <option key={option.id} value={option.id}>
                    {option.name}
                </option>
            )
        });

        options.unshift((<option key='asdf'></option>))

        return <div>
            <span>{this.props.title}</span>
            <select id={this.props.selectedItem.id} 
                    className='form-control'
                    value={this.props.selectedItem.id}
                    onChange={e => this.props.selectItem(this.props.items.filter(x => x.id === e.target.value)[0])}>
                {options}
            </select>
      </div>;
    }
            
    show() {
        this.setState({ listVisible: true });
        document.addEventListener("click", this.hide);
    }
            
    hide() {
        this.setState({ listVisible: false });
        document.removeEventListener("click", this.hide);
    }
}
