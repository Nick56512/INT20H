import Loading from "../../atoms/Loading"
import "../../styles/css/index.css"
import { useState, useEffect, useMemo } from "react"
import useInput from "../../hooks/useInput"
import ListForCategories from "../../atoms/ListForCategories"

function Projects(){
		let projs = [
			{name: 'YouTube',
			description: 'Try to copy a video viewing platform',
			categories: [{name: 'Design', id:1}, {name: 'Frontend', id:2}, {name: 'C#', id:3}],
			id: 1
			},
			{name: 'Facebook 2.0',
			description: 'Social network by the most monopilistic company',
			categories: [{name: 'С#', id:2}, {name: 'Devops', id:3}],
			id: 2
			},
			{name: 'Telegram',
			description: 'Messanger',
			categories: [{name: 'C++', id:2}, {name: 'Frontend', id:3}],
			id: 3
			},
			{name: 'Copy of Spotify',
			description: 'ahgdgergerahaha',
			categories: [{name: 'C#', id:2}, {name: 'Swift', id:3}],
			id: 4
			},
			{name: 'GitLab',
			description: 'ahgdgergerahaha',
			categories: [{name: 'C++', id:2}, {name: 'Mobile Develop', id:3}],
			id: 5
			},
		]

		const [projects, setProjects] = useState(projs)
		const [categories, setCategories] = useState([{name: 'Frontend'}, {name: 'Backend'},{name: 'C++'},{name: 'C#'},{name: 'Mobile Develop'},{name: 'Devops'},{name: 'Swift'},{name: 'Design'}])
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
			return projs.filter(project => {
				return project.categories.some(catr => catr.name === cat);
			});
	
		}, [projects, cat])

		const sortedAndSearched = useMemo(() => {
			if (filteredPosts){
				return filteredPosts.filter(item => item.name.toLowerCase().includes(searchQuery.value.toLowerCase()))
			}
			
		}, [searchQuery, filteredPosts])
		
			

	useEffect(() => {
    cut()
	}, [])



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