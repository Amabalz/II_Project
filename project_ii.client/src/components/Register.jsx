function Register() {
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "50vh",
      }}
    >
      <form>
        <h5>Register</h5>
        <div className="mb-3">
          <input
            type="text"
            className="form-control"
            id="inputUsernameRegister"
            placeholder="Enter username"
          />
        </div>
        <div className="mb-3">
          <input
            type="email"
            className="form-control"
            id="inputEmailRegister"
            placeholder="Enter email"
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            id="inputPasswordRegister"
            placeholder="Enter password"
          />
        </div>
        <button
          type="submit"
          className="btn btn-dark"
          style={{ position: "relative" }}
        >
          Register
        </button>
      </form>
    </div>
  );
}

export default Register;
