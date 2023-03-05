import { useLocation, Navigate } from "react-router-dom";

import { useStateContext } from '../context/context'

function RequireAuth({children}){

	const {isAuth} = useStateContext();
	const location = useLocation()

	if(isAuth === false){
		return <Navigate to={'/signIn'} state={{from: location}}/>
	}
	return children
}

export default RequireAuth