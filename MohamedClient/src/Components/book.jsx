import React, { useState, useEffect } from "react";
import axios from "axios";
//c# api book url
const API_URL = "http://localhost:5163/livres";

export function Book() {
  const [books, setBooks] = useState([]);

  const [createTitle, setCreateTitle] = useState("");
  const [createAuthor, setCreateAuthor] = useState("");
  const [createMediaType, setCreateMediaType] = useState("");
  const [searchAuthor, setSearchAuthor] = useState("");
  const [searchTitle, setSearchTitle] = useState("");

  const [modifyId, setModifyId] = useState(null);
  const [modifyTitle, setModifyTitle] = useState("");
  
  const [modifyAuthor, setModifyAuthor] = useState("");
  const [modifyMediaType, setModifyMediaType] = useState("");

  const [showModifyForm, setShowModifyForm] = useState(false);
//either fetchs searched sorted or all books
  const fetchBooks = async (author = "", title = "") => {
    try {
      let url = API_URL;
      //add author or  title or sort to query
      if (author || title) {
        url += "?";
        if (author) url += `Author=${encodeURIComponent(author)}&`;
        if (title) url += `Title=${encodeURIComponent(title)}&`;
        url = url.slice(0, -1);
      }
      const response = await axios.get(url);
      setBooks(response.data);
    } catch {
      alert("no books were found");
    }
  };

  useEffect(() => {
    fetchBooks();
  }, []);
//create a new book in bd
  const createNewBook = async (e) => {
    e.preventDefault();
    if (!createTitle || !createAuthor || !createMediaType) {
      alert("Please fill all of the book fieldss");
      return;
    }
    try {
      await axios.post(API_URL, {
        title: createTitle,
        author: createAuthor,
        mediaType: createMediaType,
      });
      setCreateTitle("");
      setCreateAuthor("");
      setCreateMediaType("");
      fetchBooks(searchAuthor, searchTitle);
    } catch {
      alert("failed to create new book");
    }
  };
  // delete book from db
   const deleteBook = async (id) => {
    try {
      await axios.delete(`${API_URL}/${id}`);
      fetchBooks(searchAuthor, searchTitle);
    } catch {
      alert("Failed to delete book");
    }
  };
  //get previous book field to show them in modify form
  const handelModifyForm = (book) => {
  
    setModifyId(book.id);
    setModifyTitle(book.title);
    setModifyAuthor(book.author);
    setModifyMediaType(book.type); 
    setShowModifyForm(true);
  };

  const ModifyBook = async (e) => {
    e.preventDefault();
    if (!modifyTitle || !modifyAuthor) {
      alert("please fill all of the  modify form fields");
      return;
    }
    try {
      await axios.put(`${API_URL}/${modifyId}`, {
        title: modifyTitle,
        author: modifyAuthor,
        mediaType: modifyMediaType.toLowerCase(), // send as it was
      });
      setShowModifyForm(false);
      setModifyId(null);
      fetchBooks(searchAuthor, searchTitle);
    } catch {
      alert("Failed to modify the book");
    }
  };

  const searchBook = (e) => {
    e.preventDefault();
    fetchBooks(searchAuthor, searchTitle);
  };

  const getAllBooks = () => {
    setSearchAuthor("");
    setSearchTitle("");
    fetchBooks();
  };

  return (
    <div>
      <h1>Library Client</h1>

      <form onSubmit={searchBook}>
        <input
          placeholder="Search with author"
          value={searchAuthor}
          onChange={(e) => setSearchAuthor(e.target.value)}
        />
        <input
          placeholder="Search with title"
          value={searchTitle}
          onChange={(e) => setSearchTitle(e.target.value)}
        />
        <button type="submit">Search</button>
        <button type="button" onClick={getAllBooks}>
         Get All books
        </button>
      </form>

      <h2>Add New Book</h2>
      {/* add book form */}
      <form onSubmit={createNewBook}>
        <input
          placeholder="Title"
          value={createTitle}
          onChange={(e) => setCreateTitle(e.target.value)}
        />
        <input
          placeholder="Author"
          value={createAuthor}
          onChange={(e) => setCreateAuthor(e.target.value)}
        />
        <select
          value={createMediaType}
          onChange={(e) => setCreateMediaType(e.target.value)}
        >
          <option value="">Select Type</option>
          <option value="ebook">Ebook</option>
          <option value="paperbook">PaperBook</option>
        </select>
        <button type="submit">Create</button>
      </form>
      {/* show all books */}
      <h2>Books</h2>
      {books.length === 0 && <p>No books found.</p>}
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <b>{book.title}</b> by {book.author} ({book.type})
            <button onClick={() => handelModifyForm(book)}>Modify</button>
            <button onClick={() => deleteBook(book.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {/* modify books form*/}
      {showModifyForm && (
        <>
          <h2>Modify Book</h2>
          <form onSubmit={ModifyBook}>
            <input
              placeholder="Title"
              value={modifyTitle}
              onChange={(e) => setModifyTitle(e.target.value)}
            />
            <input
              placeholder="Author"
              value={modifyAuthor}
              onChange={(e) => setModifyAuthor(e.target.value)}
            />
            <p>Media Type: {modifyMediaType}</p>
            <button type="submit">Modify</button>
            <button
              type="button"
              onClick={() => {
                setShowModifyForm(false);
                setModifyId(null);
              }}
            >
              Cancel
            </button>
          </form>
        </>
      )}
    </div>
  );
}
