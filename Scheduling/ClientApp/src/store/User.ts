import { type } from 'os';
import { Action, Reducer } from 'redux';

export interface UserState {
    logged: boolean,
    user: UserData
}

export type UserData = { login: string, password: string, name: string,
    surname: string, position: string, department: string, email: string } | null;

export interface LogInUserAction { type: 'LOGIN_USER', logData: UserData }
export interface LogOutUserAction { type: 'LOGOUT_USER' }

export type KnownAction = LogInUserAction | LogOutUserAction;

export const actionCreators = {
    logIn: (logData: UserData) => ({ type: 'LOGIN_USER', logData: logData } as LogInUserAction),
    logOut: () => ({ type: 'LOGOUT_USER' } as LogOutUserAction)
};

export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {
    if (state === undefined) {
        console.log(localStorage.getItem('logged'), localStorage.getItem('user'));
        let log = localStorage.getItem('logged');
        let logged = false;
        let user;
        let userInfo;
        if(log){
            logged = log.toLocaleLowerCase() === 'true' ? true : false;
            userInfo = localStorage.getItem('user');
            if(userInfo !== null)
                user = JSON.parse(userInfo)
            console.log(logged, user);
        }
        return { logged: logged, user: user };
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'LOGIN_USER':
            if (action.logData !== null) {
                localStorage.setItem('logged', 'true');
                localStorage.setItem('user', JSON.stringify(action.logData));
                return { logged: true, user: action.logData };
            }
            else {
                return { logged: false, user: null  };
            }
        case 'LOGOUT_USER':
            localStorage.clear();
            return { logged: false, user: null  };
        default:
            return state;
    }
};