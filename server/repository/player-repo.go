package repository

import (
	"context"
	"fmt"

	"cloud.google.com/go/firestore"
	"github.com/Atul-Acharya-17/Mind-Zone/entity"
	"google.golang.org/api/iterator"
)

type PlayerRepository interface {
	Save(player entity.Player) (*entity.Player, error)
	FindAll() ([]entity.Player, error)
	Update(player entity.Player) (*entity.Player, error)
}

type repo struct{}

func NewPlayerRepository() PlayerRepository {
	return &repo{}
}

const (
	projectID      string = "mind-zone"
	collectionName string = "players"
)

func (*repo) Save(player entity.Player) (*entity.Player, error) {
	ctx := context.Background()
	client, err := firestore.NewClient(ctx, projectID)
	if err != nil {
		fmt.Println("Could not connect to firestore")
		return nil, err
	}

	defer client.Close()

	players := client.Collection(collectionName)

	_, err = players.Doc(player.Name).Set(ctx, player)
	if err != nil {
		fmt.Println("Failed to add new record")
		return nil, err
	}

	return &player, nil

}

func (*repo) FindAll() ([]entity.Player, error) {
	ctx := context.Background()
	client, err := firestore.NewClient(ctx, projectID)
	if err != nil {
		fmt.Println("Could not connect to firestore")
		fmt.Println(err)
		return nil, err
	}
	defer client.Close()

	var players []entity.Player

	iter := client.Collection(collectionName).Documents(ctx)
	defer iter.Stop()

	for {
		doc, err := iter.Next()

		if err == iterator.Done {
			break
		}

		if err != nil {
			fmt.Println("Could not iterate list of players")
			fmt.Println(err)
			return nil, err
		}
		var player entity.Player
		doc.DataTo(&player)
		players = append(players, player)
	}

	return players, nil
}

func (*repo) Update(player entity.Player) (*entity.Player, error) {
	ctx := context.Background()
	client, err := firestore.NewClient(ctx, projectID)
	if err != nil {
		fmt.Println("Could not connect to firestore")
		return nil, err
	}
	defer client.Close()

	players := client.Collection(collectionName)

	_, err = players.Doc(player.Name).Update(ctx, []firestore.Update{{Path: "MaxScore", Value: player.MaxScore}, {Path: "Scores", Value: player.Scores}})

	if err != nil {
		fmt.Println("Could not update record")
		fmt.Print(err)
		return nil, err
	}

	return &player, nil
}
