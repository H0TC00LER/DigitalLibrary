import { useLoaderData } from "react-router-dom";

export const BooksLoader = async () => {
    const res = await fetch(`${process.env.REACT_APP_BASE_URL}books`);
    const resJson = await res.json();
  
    return resJson;
};


export const BooksPage = () => {
    const result = useLoaderData();
    console.log(result)
    return(
        <>
            <p>Books</p>
            <p>Books</p>
            <p>Books</p>
            <p>Books</p>
            <p>Books</p>
            <p>Books</p>
            <p>Books</p>
        </>
    )
}

export default BooksPage