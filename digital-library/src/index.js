import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Root from './Pages/Root';
import ErrorPage from './Pages/ErrorPage';
import { BooksPage, BooksLoader } from './Pages/BooksPage';
import BookPage from './Pages/BookPage';
import MainPage from './Pages/MainPage';
import { MainPageLoader } from './Pages/MainPage';
import {LoginLoader, LoginPage} from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "books/",
        element: <BooksPage />,
        loader: BooksLoader
      },
      {
        path: "books/:id",
        element: <BookPage />,
      },
      {
        path: "/",
        element: <MainPage />,
        loader: MainPageLoader
      },
      {
        path: "login",
        element: <LoginPage />,
        loader: LoginLoader
      },
      {
        path: "register",
        element: <RegisterPage />
      }
    ],
  },
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
