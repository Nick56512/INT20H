import Loading from "../../atoms/Loading"
import "../../styles/css/index.css"
import { useState, useEffect, useMemo } from "react"
import useInput from "../../hooks/useInput"
import ListForCategories from "../../atoms/ListForCategories"

function Projects(){

		const [projects, setProjects] = useState()
		const [categories, setCategories] = useState([])
		const [catForList, setCatForList] = useState([])
		const [cat, setCat] = useState('Усі')
		
		const searchQuery = useInput('')

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
			return projects.filter(project => {
				return project.categories.some(catr => catr.name === cat);
			});
	
		}, [projects, cat])

		const sortedAndSearched = useMemo(() => {
			if (filteredPosts){
				return filteredPosts.filter(item => item.name.toLowerCase().includes(searchQuery.value.toLowerCase()))
			}
			
		}, [searchQuery, filteredPosts])

	async function fetchCategories() {
      const response = await fetch('http://mirik297-001-site1.ftempurl.com/getallcategories')
      const data = await response.json()
      setCategories(data)
		console.log(categories)
		cut()
    }

	 async function fetchProjects() {
      const response = await fetch('http://mirik297-001-site1.ftempurl.com/getAllProjects')
      const data = await response.json()
		console.log(data)
      setProjects(data)

    }
			

	useEffect(() => {
    fetchCategories();
	 fetchProjects();
	}, [catForList])



	return(
		<div className="container projects-page-main">
			<div className="projects-page projects-page__block-1">
				<input style={{marginBottom: '20px'}} value={searchQuery.value} onChange={searchQuery.onChange} placeholder="Пошук проєкту.." type="text" />
				<ListForCategories  onClick={cut} setSelected={setCat} items={catForList}></ListForCategories>
			</div>
			<div className="projects-page projects-page__block-2">
				
				{sortedAndSearched ? sortedAndSearched.map((item) => (
						<div className="project" key={item.id}>
							<h4 className="project-title">{item.name}</h4>
							<h5 className="project-description">{item.description}</h5>
							<div className="project-categories">
								{item.categories.map((item2) => (
									<div key={item2.id} className="project-categories-item">{item2.name}</div>
								))}
							</div>
						</div>
					))
				: null}
					
			</div>
			
		</div>
	)

}

export default Projects