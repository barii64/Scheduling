export interface VacationRequestState {
	logged: boolean,
	token: string | null,
	requestHistory: Array<TimerType>
}

export type TimerType = {
	id: number,
	DateTimeStart: Date, 
	DateTimeFinish: Date,
	Time: string
}