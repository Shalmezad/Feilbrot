#!/bin/bash

img_size=2048
iterations=1000
brotnames=("Chicken" "Circle" "Mandelbrot" "MantaRay")
colornames=("GrayCyan" "Invert" "Sin")

for brotname in "${brotnames[@]}"
do
    file=docs/renders/2d/$brotname.png
    if [[ ! -e "$file" ]]
    then
        echo "Rendering $file"
        dotnet run --project Feilbrot \
            --brotname $brotname \
            --iterations $iterations \
            --img-width $img_size --img-height $img_size \
            --outputfile $file
    fi
done

for colorname in "${colornames[@]}"
do
    file=docs/renders/Colorschemes/$colorname.png
    if [[ ! -e "$file" ]]
    then
        echo "Rendering $file"
        dotnet run --project Feilbrot \
            --colorname $colorname \
            --iterations $iterations \
            --img-width $img_size --img-height $img_size \
            --outputfile $file
    fi
done
