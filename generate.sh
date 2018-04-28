prepend () {
    echo "$2" | cat - "$1" > "$1.tmp" && mv "$1.tmp" "$1"
}

curl https://raw.githubusercontent.com/apache/parquet-format/master/src/main/thrift/parquet.thrift -o parquet.thrift
prepend parquet.thrift 'namespace netcore Parquet.Thrift'

thrift --gen netcore -out src/ parquet.thrift

for f in src/Parquet/Thrift/*.cs; do
    prepend $f '// <auto-generated/>'
done