import Header from "../components/Header"
import Footer from "../components/Footer"
import { Outlet } from "react-router-dom"
import "./Styles/root.css"

const Root = () => {
    return(
        <>
            <Header />
            <div className="segment page-content">
                <Outlet />
            </div>
            <Footer />
        </>
    )
}

export default Root