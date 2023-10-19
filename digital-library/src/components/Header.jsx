import "../components/Styles/header.css"
import { Link } from "react-router-dom";

const Header = () => {
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
                <a href="#">User Account</a>
            </div>
        </header>
    );
}

export default Header;