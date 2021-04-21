import { TimerType } from "./types";

export interface SetHistoryAction { type: 'SET_HISTORY', requests: Array<TimerType> }
export interface CheckUserAction { type: 'CHECK_USER'}

const setHistory = (requests: Array<TimerType>) => ({ type: 'SET_HISTORY', requests: requests } as SetHistoryAction);
const checkUser =  () => ({ type: 'CHECK_USER'} as CheckUserAction);

export const addTime = time => ({
	type: "ADD_TIME",
	payload: {
		time
	}
});

export const deleteTime = time_idx => ({
	type: "DELETE_TIME",
	payload: {
		time_idx
	}
});


export const actionCreators = {
	setHistory,
	checkUser
};

export type KnownAction = SetHistoryAction | CheckUserAction;