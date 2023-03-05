import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Header from './sections/Header/Header';
import './styles/css/index.css'
import { StateContext } from './context/context'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div>
        <StateContext>
          <Header/>
            <div>
              <Routes>
                {AppRoutes.map((route, index) => {
                  const { element, ...rest } = route;
                  return <Route key={index} {...rest} element={element} />;
                })}
              </Routes>
            </div>
        </StateContext>
      </div>
    );
  }
}
