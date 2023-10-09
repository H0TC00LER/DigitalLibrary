import React from 'react';
import { useEffect, useState } from 'react';

const AuthorCard = ({ author }) => {
    const { firstName, description, photoId, writtenBooksIds } = author;
    const [image, setImage] = useState('');

    useEffect(() => {
        fetch(`https://localhost:7260/images/authorphotos/${photoId}`)
          .then(response => response.json())
          .then(data => setImage(data))
          .catch(error => console.error(error));
      }, []);

      console.log(image);

    var base64Image = `data:image/png;base64,${image}`;

    return (
    <div className="author-card">
        <img ng-src={`data:image/png;base64,${image}`} alt={firstName} />
        <h3>{firstName}</h3>
        <h4>{description}</h4>
        <ul>
        {writtenBooksIds.map((book, index) => (
            <li key={index}>{book}</li>
        ))}
        </ul>
    </div>
    );
};

export default AuthorCard