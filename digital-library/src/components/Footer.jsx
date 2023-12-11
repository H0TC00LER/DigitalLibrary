import "../components/Styles/footer.css"
import { Link } from "react-router-dom"

const Footer = () => {
    return(
        <footer className="footer segment">
            <nav>
                <ul>
                    <li><Link to={`/`}>Главная страница</Link></li>
                    <li><Link to={`/books`}>Книги</Link></li>
                    <li><Link to={`/authors`}>Авторы</Link></li>
                    <li><Link to={`/genres`}>Жанры</Link></li>
                </ul>
            </nav>
            <div className="footer_contacts">
                <a href="mailto:example@example.com">Email</a>
                <a href="tel:+1234567890">Phone</a>
            </div>
        </footer>
    )
}

export default Footer
