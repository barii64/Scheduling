
export const authenticate = async (login: string, passsword: string) => {
	const query = JSON.stringify({
		query: `mutation {
			authentication (email: "${login}" password: "${passsword}")
		}`
	});

 return fetch('/graphql', {
		method: 'POST',
		headers: {'content-type': 'application/json'},
		body: query
	})
	.then(data => data.json());
};