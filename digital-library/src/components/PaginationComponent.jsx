import "./Styles/pagination-component.css"

const PaginationComponent = ({totalItems, limit, onPageChange}) => {
    const totalPages = Math.ceil(totalItems / limit);
    const pages = [];
    for(let i = 1; i <= totalPages; i++){
        pages.push(<button onClick={() => handleClick(i)} key={i}>{i}</button>);
    }

    const handleClick = (pageNumber) => {
        // Call the onPageChange function and pass the clicked page number
        onPageChange(pageNumber);
    };

    return(
        <div className="pagination-container">
            <div className="pages-list">
                {pages}
            </div>
            <button>
                Previous Page
            </button>
            <button> 
                Next Page
            </button>
        </div>
    )
}

export default PaginationComponent