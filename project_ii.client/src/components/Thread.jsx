import { useState, useEffect } from "react";
import Comment from "./Comment";
const Thread = ({ username, content, likes, dislikes }) => {
  const [likeCount, setLikeCount] = useState(likes);
  const [dislikeCount, setDislikeCount] = useState(dislikes);
  const [comments, setComments] = useState([]);

  useEffect(() => {
    fetchComments();
  }, []);
  // posibil trebuie refacut, nu stiu daca merge asa =))
  const fetchComments = async () => {
    try {
      const response = await fetch("your-api-endpoint");
      const data = await response.json();
      setComments(data.comments);
    } catch (error) {
      console.error("Error fetching comments:", error);
    }
  };

  const handleLike = () => {
    setLikeCount(likeCount + 1);
  };

  const handleDislike = () => {
    setDislikeCount(dislikeCount + 1);
  };

  const handleAddComment = (comment) => {
    setComments([...comments, comment]);
  };

  const [newComment, setNewComment] = useState("");

  const handleInputChange = (event) => {
    setNewComment(event.target.value);
  };

  // trebuie schimbat sa trimita in api si dam fetch iar din api sa aducem comentariile sau idk, nu stiu cum sa fac
  const handleSubmit = () => {
    if (newComment.trim() !== "") {
      handleAddComment({
        name: "New Comment",
        content: newComment,
      });
      setNewComment("");
    }
  };

  return (
    <div
      style={{
        background: "#1895D7", // schimba culoarea
        margin: "10px 10px 10px 10px",
        border: "1px solid black",
        borderRadius: "10px",
        padding: "10px",
      }}
    >
      <h3>Created by: {username}</h3>
      <p>{content}</p>
      <div>
        <button
          style={{
            backgroundColor: "#ffffff",
            color: "#000000",
            borderRadius: "5px",
            border: "none",
            fontSize: "16px",
            cursor: "pointer",
            margin: "0px 10px",
          }}
          onClick={handleLike}
        >
          Like ({likeCount})
        </button>
        <button
          onClick={handleDislike}
          style={{
            backgroundColor: "#ffffff",
            color: "#000000",
            borderRadius: "5px",
            border: "none",
            fontSize: "16px",
            cursor: "pointer",
            margin: "0px 10px",
          }}
        >
          Dislike ({dislikeCount})
        </button>
      </div>
      <div>
        <h4>Comments:</h4>{" "}
        {/* posibil necesita schimbare, vedem dupa implementare cu api */}
        {comments.map((comment, index) => (
          <Comment
            key={index}
            name={comment.name}
            content={comment.content}
            likes={comment.likes}
            dislikes={comment.dislikes}
          />
        ))}
      </div>
      <div>
        <input
          placeholder="Add a comment..."
          type="text"
          value={newComment}
          required
          onChange={handleInputChange}
          style={{
            width: "100%",
            height: "50px",
            borderRadius: "10px",
            padding: "10px",
            margin: "10px 0px",
          }}
        />
        <button
          onClick={handleSubmit}
          style={{
            backgroundColor: "#ffffff",
            color: "#000000",
            borderRadius: "5px",
            border: "none",
            fontSize: "16px",
            cursor: "pointer",
            margin: "0px 5px",
          }}
        >
          Add Comment
        </button>
      </div>
    </div>
  );
};

export default Thread;
