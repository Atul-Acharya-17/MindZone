package entity

type PlayerScore struct {
	Name           string  `json:"name"`
	Score          int     `json:"score"`
	AttentionLevel float64 `json:"attentionLevel"`
}
