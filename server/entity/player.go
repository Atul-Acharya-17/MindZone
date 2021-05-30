package entity

type Player struct {
	Name     string `json:"name"`
	Scores   []int  `json:"scores"`
	MaxScore int    `json:"maxScore"`
}
