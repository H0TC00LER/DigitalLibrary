import { AxiosInstance } from "../AxiosInstance";
import { useEffect, useState } from "react";
import "./Styles/book-card.css"

const BookCard = ({ imageUrl, title, description }) => {
    const [image, setImage] = useState('');

    useEffect(() => {
        fetch(`${process.env.REACT_APP_BASE_URL}images/covers/${imageUrl}`)
        .then((response) => response.blob())
        .then((blob) => URL.createObjectURL(blob))
        .then((objectUrl) => setImage(objectUrl));
      }, []);

    console.log(image)
    
    return(
        <div className="book-card">
            <img src={image} alt="image"/>
            <h4>{title}</h4>
            <h5>{description}</h5>
        </div>
    )
}

export default BookCard