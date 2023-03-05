import {Link, useMatch} from 'react-router-dom'

function MyNavLink({children, to, ...props}){

	const match = useMatch(to)

	return(
		<Link
		to={to}
		{...props}>
		{children}
		</Link>
	)
}

export default MyNavLink