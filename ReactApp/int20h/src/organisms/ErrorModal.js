function ErrorModal(props){

	return(
			<div className={`${(props.modal) ? 'overlay animated show' : 'overlay animated hide'}`} 
			onClick={() => props.setModal(false)}>
				<div className="errors">
					<ol>
						{props.error
							?
							props.error.map(item => (
								<li key={item}>{item}</li>
							))
							:
							null
						}
					</ol>
				</div>
			</div>
	)
}

export default ErrorModal