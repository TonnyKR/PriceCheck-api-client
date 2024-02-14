import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import Logo from './logo.jsx'
import Search from './Search.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Logo />
    <Search/>
  </React.StrictMode>,
)
