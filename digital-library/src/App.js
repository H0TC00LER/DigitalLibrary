import AuthorCard from './components/AuthorCard'
import './App.css';
import React, { useEffect, useState } from 'react';

function App() {
  const [authors, setAuthors] = useState([]);

  useEffect(() => {
    fetch('https://localhost:7260/authors')
      .then(response => response.json())
      .then(data => setAuthors(data))
      .catch(error => console.error(error));
  }, []);

  console.log(authors)

  return (
    <div className="App">
      {authors.map((author, index) => (
        <AuthorCard key={index} author={author} />
      ))}
    </div>
  );
}

export default App;
