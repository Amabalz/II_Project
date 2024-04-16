function Login() {
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
        <h5>Login</h5>
        <div className="mb-3">
          <input
            type="email"
            className="form-control"
            id="inputEmailLogin"
            placeholder="Enter email"
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            id="inputPasswordLogin"
            placeholder="Enter password"
          />
        </div>

        {/*add here remember me checkbox*/}

        <button
          type="submit"
          className="btn btn-dark"
          style={{ position: "relative" }}
        >
          Login
        </button>
      </form>
    </div>
  );
}

export default Login;
