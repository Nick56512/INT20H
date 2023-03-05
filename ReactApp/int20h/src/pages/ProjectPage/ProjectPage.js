import Loading from "../../atoms/Loading"
import "../../styles/css/index.css"
import { useState, useEffect } from "react"

function ProjectPage(){

	const [recipes, setRecipes] = useState([])
	const [isLoading, setLoading] = useState(false)

	useEffect(() => {
		getRecipes()
	}, [])


	async function getRecipes(){
			setLoading(true)
			setTimeout(async () => {
				console.log('hghgf')
			}, 300)
		}

	return(
		<div>

		</div>
	)

}

export default ProjectPage