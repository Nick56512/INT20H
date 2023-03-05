import {useState} from "react";

function useInput(Value){

	function clear(){
		setValue('')
	}

	const [value, setValue] = useState(Value)
	const onChange = event => {
		setValue(event.target.value)
	}

	const onChangeFile = event => {
		setValue(event.target.files[0])
	}
	
	return {
		value, onChange, onChangeFile, clear
	}
}

export default useInput