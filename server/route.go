package main

import (
	"encoding/json"
	"fmt"
	"net/http"

	//"github.com/Atul-Acharya-17/Mind-Zone/entity"
	"github.com/Atul-Acharya-17/Mind-Zone/entity"
	"github.com/Atul-Acharya-17/Mind-Zone/repository"
)

var (
	repo repository.PlayerRepository = repository.NewPlayerRepository()
)

func getLeaderboard(res http.ResponseWriter, req *http.Request) {
	res.Header().Set("Content-Type", "application/json")

	players, err := repo.FindAll()

	if err != nil {
		res.WriteHeader(http.StatusInternalServerError)
		res.Write([]byte(`{Error Retrieving players}`))
		return
	}

	var playerScores []entity.PlayerScore

	for i := range players {
		playerScore := entity.PlayerScore{Name: players[i].Name, Score: players[i].MaxScore}

		playerScores = append(playerScores, playerScore)
	}

	sortedPlayerScores := sort(playerScores)

	size := 5

	if size > len(sortedPlayerScores) {
		size = len(sortedPlayerScores)
	}

	res.WriteHeader(http.StatusOK)
	json.NewEncoder(res).Encode(sortedPlayerScores[:size])
}

func saveScore(res http.ResponseWriter, req *http.Request) {
	res.Header().Set("Content-Type", "application/json")

	var playerScore entity.PlayerScore

	err := json.NewDecoder(req.Body).Decode(&playerScore)

	if err != nil {
		res.WriteHeader(http.StatusInternalServerError)
		res.Write([]byte(`{Error Marshalling the request}`))
		fmt.Print(err)
		return
	}

	players, err := repo.FindAll()

	if err != nil {
		res.WriteHeader(http.StatusInternalServerError)
		res.Write([]byte(`{Error Retrieving players}`))
		return
	}

	for i := range players {
		if players[i].Name == playerScore.Name {
			players[i].Scores = append(players[i].Scores, playerScore.Score)
			players[i].AttentionLevels = append(players[i].AttentionLevels, playerScore.AttentionLevel)
			if players[i].MaxScore < playerScore.Score {
				players[i].MaxScore = playerScore.Score
			}
			repo.Update(players[i])
			result, err := json.Marshal(players[i])
			if err != nil {
				res.WriteHeader(http.StatusInternalServerError)
				res.Write([]byte(`{Error Marshalling}`))
				return
			}
			res.WriteHeader(http.StatusOK)
			res.Write(result)
			return
		}
	}

	fmt.Println(playerScore.AttentionLevel)
	var player = entity.Player{Name: playerScore.Name, Scores: []int{playerScore.Score}, MaxScore: playerScore.Score, AttentionLevels: []float64{playerScore.AttentionLevel}}
	repo.Save(player)

	result, err := json.Marshal(player)
	if err != nil {
		res.WriteHeader(http.StatusInternalServerError)
		res.Write([]byte(`{Error Marshalling}`))
		return
	}
	res.WriteHeader(http.StatusOK)
	res.Write(result)
}

func sort(playerScores []entity.PlayerScore) []entity.PlayerScore {
	for i := 1; i < len(playerScores); i++ {
		for j := i; j > 0; j-- {
			if playerScores[j].Score > playerScores[j-1].Score {
				temp := playerScores[j]
				playerScores[j] = playerScores[j-1]
				playerScores[j-1] = temp
			}
		}
	}
	return playerScores
}
