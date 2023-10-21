// export async function action() {
//     const contact = await createContact();
//     return { contact };
// }

export const BooksLoader = async () => {
    // const requestOptions = {
    //     method: 'POST',
    //     headers: { 'Content-Type': 'application/json' },
    //     body: JSON.stringify({ 
    //         SearchWord,
    //         BookTags,
    //         SortBy,
    //         Page,
    //         Limit,
    //      })
    // };

    const res = await fetch(`${process.env.REACT_APP_BASE_URL}books`);
    const resJson = await res.json();
  
    return resJson;
};

const CreateBookPage = () => {
    return(
        <Form method="post" action="/events">
            <input type="text" name="title" />
            <input type="text" name="description" />
            <input type="date" name="publicationDate" />
            <input type="image" name="image" />
            <input type="file" name="book" />
            <button type="submit">Create</button>
        </Form>
    )
}

export default CreateBookPage