import React, { createContext, useContext, useState, useEffect } from 'react';

const Context = createContext();

export const StateContext = ({ children }) => {

	const [isAuth, setIsAuth] = useState(true)
	const [name, setName] = useState('AFDGSD')
	const [lastName, setLastName] = useState('FDDJE')
	const [hasPhoto, setHasPhoto] = useState(false)
	const [photo, setPhoto] = useState()

	return (
    <Context.Provider
      value={{
			isAuth,
			setIsAuth,
			name,
			setName,
			lastName,
			setLastName,
			hasPhoto,
			setHasPhoto
      }}
    >
      {children}
    </Context.Provider>
  )
}

export const useStateContext = () => useContext(Context);