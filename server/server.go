// Go File

package main

import (
	"fmt"
	"log"
	"net/http"

	"github.com/gorilla/mux"
)

func main() {
	const port string = ":8000"

	router := mux.NewRouter()
	router.HandleFunc("/", func(resp http.ResponseWriter, req *http.Request) {
		fmt.Fprintln(resp, "Up and Running")
	})
	router.HandleFunc("/players", getLeaderboard).Methods("GET")
	router.HandleFunc("/players", saveScore).Methods("POST")
	log.Println("Listening on port ", port)
	http.ListenAndServe(port, router)
}
