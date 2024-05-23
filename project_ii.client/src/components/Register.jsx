import React, { useState } from 'react';

function Register() {
  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Save data to local storage
    localStorage.setItem('user', JSON.stringify(formData));
    console.log('User registered:', formData);
    // Clear the form
    setFormData({
      username: '',
      email: '',
      password: '',
    });
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "50vh",
      }}
    >
      <form onSubmit={handleSubmit}>
        <h5>Register</h5>
        <div className="mb-3">
          <input
            type="text"
            className="form-control"
            id="inputUsernameRegister"
            name="username"
            placeholder="Enter username"
            value={formData.username}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <input
            type="email"
            className="form-control"
            id="inputEmailRegister"
            name="email"
            placeholder="Enter email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            id="inputPasswordRegister"
            name="password"
            placeholder="Enter password"
            value={formData.password}
            onChange={handleChange}
            required
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
