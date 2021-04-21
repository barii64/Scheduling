import * as React from 'react';
import { VacationRequest } from '../store/VacationRequest/types';
import '../style/RequestsTable.css';

type TableProps = {
    requests: Array<VacationRequest>
}

export const TimerHistoryTable: React.FunctionComponent<TableProps> = ({ requests }) => {
    if(requests.length>0)
        console.log('table' + requests[0].comment);
    return (
        <React.Fragment>
            <div id='vacation-history'>
                <h5>Vacation history</h5>
                <table id='history'>
                    <tbody>
                        <tr>
                            <th>Interval</th>
                            <th>Time</th>
                            <th></th>
                        </tr>
                        {requests.map((r) => <tr key={requests.indexOf(r)}>
                            <td>{(r.startDate).toDateString()}-{r.finishDate.toDateString()}</td>
                            <td>{r.status}</td>
                            <td><button disabled={!r.editable}>Delete</button></td>
                        </tr>)}
                        <button id='send-request'>Add new item</button>
                    </tbody>
                </table>
            </div>
        </React.Fragment>
    );
}