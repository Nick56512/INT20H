import "../styles/css/index.css"

import { useState } from 'react';

function ListForCategories(props) {
  let list = props.items

  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState('Усі');

  const toggleDropdown = () => setIsOpen(!isOpen);

  const handleOptionClick = (item) => {
    setSelectedOption(item);
    toggleDropdown();
    props.setSelected(item)
  };

  return (
    <div className="dropdown">
      <div className="dropdown-button" onClick={toggleDropdown}>
        {selectedOption}  
		  <span style={{fontSize: '13px'}}>  {isOpen ? '   ▼' : '   ▲'}</span>
      </div>
      <ul className={`dropdown-list ${isOpen ? "open" : ""}`}>
        {list.map((item) => (
          <li key={item} onClick={() => handleOptionClick(item)}>
            {item}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ListForCategories;