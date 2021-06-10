package entity

type Player struct {
	Name            string    `json:"name"`
	Scores          []int     `json:"scores"`
	AttentionLevels []float64 `json:"attentionLevels"`
	MaxScore        int       `json:"maxScore"`
}
