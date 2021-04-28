
export const getUserData = async (token: string) => {
	const query = JSON.stringify({
		query: `{
			getUser{
				name
				surname
				email
				position
				department
				permissions
			}
		}`
	});
  
	return fetch('/graphql', {
		method: 'POST',
		headers: {
			'content-type': 'application/json',
			'Authorization': `Bearer ${token}`
		},
		body: query
	})
	.then(data => data.json());
};

export const getUserTimerData = async (token: string) => {
	const query = JSON.stringify({
		query: `{
			getUser{
				timerHistories{
					startTime
					finishTime
				}
			}
		}`
	});
  
	return fetch('/graphql', {
		method: 'POST',
		headers: {
			'content-type': 'application/json',
			'Authorization': `Bearer ${token}`
		},
		body: query
	})
	.then(data => data.json());
};