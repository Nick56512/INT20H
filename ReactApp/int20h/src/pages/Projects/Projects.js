import Loading from "../../atoms/Loading"
import "../../styles/css/index.css"
import { useState, useEffect, useMemo } from "react"
import useInput from "../../hooks/useInput"
import ListForCategories from "../../atoms/ListForCategories"

function Projects(){
		let projs = [
			{name: 'LolOlo',
			description: 'ahahaha',
			categories: [{name: 'c#', id:1}, {name: 'js', id:2}, {name: 'pascal', id:3}],
			id: 1
			},
			{name: 'MMLolOlo',
			description: 'ahgdgergerahaha',
			categories: [{name: 'js', id:2}, {name: 'pascal', id:3}],
			id: 1
			},
		]

		const [projects, setProjects] = useState(projs)
		const [filtered, setFiltered] = useState([])
		const [categories, setCategories] = useState([{name: 'c#', id:1}, {name: 'js', id:2}, {name: 'pascal', id:3}, {name: 'hdgr', id:4}, {name: 'govnoshop', id:5}])
		const [catForList, setCatForList] = useState([])
		const [undf, setUndf] = useState(false)
		const [isLoading, setLoading] = useState(false)
		const [cat, setCat] = useState('Усі')
		const [page, setPage] = useState(1)
		
		const searchQuery = useInput('')

		useEffect(() => {
			cut()
		}, [])

		function cut(){
			let time = []
			for(let i = 0; i < categories.length; i++){
				time.push(categories[i].name)
			}
			setCatForList(time)
		}

		const filteredPosts = useMemo(() => {
			if (cat === 'Усі'){
				return projects
			}
			else{
				for(let j = 0; j < projects.length; j++){
					for(let i = 0; i < projects[j].categories.length; i++){
						if(projects[j].categories[i].name === cat){
							return projects.filter(item => item.categories[i].name === cat)
						}
					}
				}
			}
		}, [projects, cat])

		const sortedAndSearched = useMemo(() => {
			if (filteredPosts === undefined){
				setUndf(true)
				return []
			}
			else{
				return filteredPosts.filter(item => item.name.toLowerCase().includes(searchQuery.value.toLowerCase()))
			}
			
		}, [searchQuery, filteredPosts])
			


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
		<div className="container projects-page-main">
			<div className="projects-page projects-page__block-1">
				<input value={searchQuery.value} onChange={searchQuery.onChange} placeholder="Пошук проєкту.." type="text" />
				<ListForCategories setSelected={setCat} items={catForList}></ListForCategories>
			</div>
			<div className="projects-page projects-page__block-2">
				
				{sortedAndSearched.map((item) => (
						<div className="project" key={item.id}>
							<h4 className="project-title">{item.name}</h4>
							<h5 className="project-description">{item.description}</h5>
							<div className="project-categories">
								{item.categories.map((item2) => (
									<div key={item2.id} className="project-categories-item">{item2.name}</div>
								))}
							</div>
						</div>
					))}
					
			</div>
			
		</div>
	)

}

export default Projects