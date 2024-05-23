import React from "react";
import ThreadCreateComp from "./ThreadCreateComp";
import Thread from "./Thread";

class Home extends React.Component {
  render() {
    return (
      <div
        style={{
          background: "#e9ecef",
          height: 1500,
          width: "100%",
          position: "relative",
        }}
      >
        <div style={{ width: "15%", height: "100%" }}>
          <div
            style={{
              //background: "orange",
              width: "60%",
              height: "100%",
              position: "absolute",
              left: "20%",
              top: "0",
            }}
          >
            {/* top:'0' optinut prin incercari repetate, autor: Ududec Puiu Dan Avram, IE anul 2*/}

            <ThreadCreateComp />
            <h2 style={{ margin: "10px 0px 20px 0px" }}>Threads:</h2>
            <Thread
              username={"Ududec Puiu Dan Avram"}
              content={"ceva aici (e hardcodat asta, vezi in Home.jsx)"}
              likes={7}
              dislikes={9} //hardcodat ca exemplu, in realitate o sa fie un fetch, cred
            />
            <Thread />
          </div>
        </div>
      </div>
    );
  }
}
export default Home;
