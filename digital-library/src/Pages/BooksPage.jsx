import { useLoaderData, Form } from "react-router-dom";
import BookCard from "../components/BookCard";
import "./Styles/books-page.css"
import PaginationComponent from "../components/PaginationComponent";

export const BooksLoader = async () => {
    const res = await fetch(`${process.env.REACT_APP_BASE_URL}books`);
    const resJson = await res.json();
  
    return resJson;
};

const changePage = (pageNumber) => {
    console.log(pageNumber)
}

export const BooksPage = (pageNumber) => {
    const result = useLoaderData();
    console.log(result)
    return(
        <div className="books-page">
            <PaginationComponent totalItems={100} limit={10} onPageChange={changePage}/>

            {result.map(book => (
                <BookCard imageUrl={book.coverUrl} title={book.title} description={book.description} />
            ))}

        </div>
    )
}

export default BooksPage