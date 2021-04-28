import Cookies from "js-cookie";
import { Action, Reducer } from "redux";
import { KnownAction } from "./actions";
import { VacationRequestState } from "./types";

const reducer: Reducer<VacationRequestState> = (state: VacationRequestState | undefined, incomingAction: Action): VacationRequestState => {
	if (state === undefined) {
		const token = Cookies.get('token');
		if(token)
			return { logged: true, token: token, requestHistory: [] };
		else
			return { logged: false, token: null, requestHistory: [] };
	}

	const action = incomingAction as KnownAction;
	switch (action.type) {
			case 'SET_HISTORY':
				if(action.requests.length > 0){
					console.log('set');
					return { logged: state.logged, token: state.token, requestHistory: action.requests };
				}
				return { logged: state.logged, token: state.token, requestHistory: [] };
				
			case 'CHECK_USER':
				const token = Cookies.get('token');
				if(token)
					return { logged: true, token: token, requestHistory: [] };
				else
					return { logged: false, token: null, requestHistory: [] };
			default:
				return state;
	}
};

export default reducer;