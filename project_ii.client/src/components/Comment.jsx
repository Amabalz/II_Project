const Comment = ({ name, comment, likes, dislikes }) => {
  const handleLike = () => {
    // adauga logica update like in api
  };

  const handleDislike = () => {
    // adauga logica update dislike in api
  };

  return (
    <div
      className="comment"
      style={{
        background: "white",
        margin: "10px 10px 10px 10px",
        border: "1px solid black",
        borderRadius: "10px",
        padding: "10px",
      }}
    >
      <p>{name} commented:</p>
      <p>{comment}</p>
      <div className="buttons">
        <button className="like-button" onClick={handleLike}>
          Like ({likes})
        </button>
        <button className="dislike-button" onClick={handleDislike}>
          Dislike ({dislikes})
        </button>
      </div>
    </div>
  );
};

export default Comment;
