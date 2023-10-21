import { useLoaderData, Form } from "react-router-dom";
import BookCard from "../components/BookCard";
import "./Styles/books-page.css"

export const BooksLoader = async () => {
    const res = await fetch(`${process.env.REACT_APP_BASE_URL}books`);
    const resJson = await res.json();
  
    return resJson;
};


export const BooksPage = () => {
    const result = useLoaderData();
    console.log(result)
    return(
        <div className="books-page">
            {result.map(book => (
                <BookCard imageUrl={book.coverUrl} title={book.title} description={book.description} />
            ))}
        </div>
    )
}

export default BooksPage