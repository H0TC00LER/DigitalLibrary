import { useLoaderData } from "react-router-dom";
import { AxiosInstance } from "../AxiosInstance";
import { BookTags } from "../BookTags.tsx";
import BookCard from "../components/BookCard.jsx";

export const MainPageLoader = async () => {
    console.log(AxiosInstance.defaults);
    const booksAmount = await AxiosInstance.get("/books/total");
    const authorsAmount = await AxiosInstance.get("/authors/total");
    const randomListByTag = await AxiosInstance.get("/books/random-list-by-tag/10");
    const randomListByAuthor = await AxiosInstance.get("/books/")
    //const authTest = await AxiosInstance.get("/books/auth-test");
  
    return {
        booksAmount: booksAmount.data,
        authorsAmount: authorsAmount.data,
        randomListByTag: randomListByTag.data,
        //authTest: authTest.data
    }
};


const MainPage = () => {
    const { booksAmount, authorsAmount, randomListByTag, authTest } = useLoaderData();

    console.log(localStorage.getItem("token"));
    console.log(authTest);

    console.log(randomListByTag);

    return(
        <div>
            <p>Добро пожаловать на сайт. Здесь вы сможете читать книги онлайн.</p>
            <p>На данный момент библиотека насчитывает {booksAmount} книг, {authorsAmount} авторов.</p>
            <p>Книги, где есть {randomListByTag.item2}</p>
            <div className="vertical-list">
                {randomListByTag.item1.map(book => 
                    <BookCard imageUrl={book.coverUrl} title={book.title} description={book.description}/>
                )}
            </div>
            
            <p>Книги за авторством {}</p>
        </div>
    )
}

export default MainPage