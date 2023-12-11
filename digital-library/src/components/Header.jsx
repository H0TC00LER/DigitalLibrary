import "../components/Styles/header.css"
import { Link } from "react-router-dom";
import { isLogged, LogoutUser } from "./AuthManager";

const Header = () => {
    const handleExit = () => {
        LogoutUser();
    }

    const UserLink = () => {
        if (isLogged) {
            return (
                <>
                    <Link to={`/userPage`}>Мой аккаунт</Link>
                    |
                    <a onClick={handleExit} href="#">Выйти</a>
                </>
                
            )
        }
        return (
            <>
                <Link to={`/register`}>Регистрация</Link>
                |
                <Link to={`/login`}>Вход</Link>
            </>
        )
    }

    return (
        <header className="segment">
            <div className="logo">
                <img src="/images/logo.png" alt="Site Logo"/>
            </div>
            <nav>
                <ul>
                    <li><Link to={`/`}>Главная страница</Link></li>
                    <li><Link to={`/books`}>Книги</Link></li>
                    <li><Link to={`/authors`}>Авторы</Link></li>
                    <li><Link to={`/genres`}>Жанры</Link></li>
                </ul>
            </nav>
            <div className="search">
                <input type="text" placeholder="Search"/>
            </div>
            <div className="user-account">
                <UserLink/>
            </div>
        </header>
    );
}

export default Header;