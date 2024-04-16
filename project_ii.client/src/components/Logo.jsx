import logo from "./logo.png"

function Logo() {
  return (
    <div
      className="vstack"
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "50vh",
        background: "#e9ecef",
      }}
    >
      <img src={logo} alt="Logo" />
      <h6 className="text-center">
        Sign up to support best cooking community!
      </h6>
    </div>
  );
}

export default Logo;
